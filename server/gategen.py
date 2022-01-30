import numpy as np

from qiskit import QuantumCircuit
from qiskit import Aer, execute


def get_random_float(xmin, xmax, backend, N=1, ndim=6):
    qc = QuantumCircuit(1, 1)
    qc.h(0)
    qc.measure(0, 0)
    job = execute([qc for _ in range(ndim * N)], backend, shots=1)
    binary = "".join([str(i.get("1", 0)) for i in job.result().get_counts()])
    randints = np.array([int(binary[i::N], 2) for i in range(N)])
    return randints / (2**ndim - 1) * (xmax - xmin) + xmin


def get_random_bool(backend):
    qc = QuantumCircuit(1, 1)
    qc.h(0)
    qc.measure(0, 0)
    job = execute(qc, backend, shots=1)
    return job.result().get_counts().get("1", 0)


def get_random_gate(rand, backend):
    if rand < 0.46:
        return {"gate": "RX", "angle": get_random_float(0, np.pi / 8, backend)[0]}
    elif rand < 0.76:
        return {
            "gate": "RX",
            "angle": get_random_float(np.pi / 8, np.pi / 4, backend)[0],
        }
    elif rand < 0.91:
        return {
            "gate": "RX",
            "angle": get_random_float(np.pi / 4, np.pi / 2, backend)[0],
        }
    elif rand < 0.99:
        if get_random_bool(backend):
            return {"gate": "H", "angle": 0}
        else:
            return {"gate": "Z", "angle": 0}
    else:
        if get_random_bool(backend):
            return {"gate": "X", "angle": 0}
        else:
            return {"gate": "Y", "angle": 0}


def get_random_gateset(N, backend):
    return [
        get_random_gate(i, backend)
        for i in get_random_float(0, 1, backend, N=N, ndim=7)
    ]
