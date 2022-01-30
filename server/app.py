from flask import Flask, request

import numpy as np

import qiskit
from qiskit import QuantumRegister, ClassicalRegister, QuantumCircuit
from qiskit import Aer, execute

from utils import vector2_to_complex, complex_to_vector2

app = Flask(__name__)

sim = Aer.get_backend("statevector_simulator")


@app.route("/qiskit_version")
def get_qiskit_version():
    return str(qiskit.__qiskit_version__)


@app.route("/evolve_soul", methods=["POST"])
def evolve_soul():
    print(request.json)
    soul = request.json.get("soul")
    gate = request.json.get("gate")
    target = request.json.get("target") == "lefty"
    angle = request.json.get("angle")

    players = QuantumRegister(2, name="players")
    is_alive = ClassicalRegister(2, name="is_alive")

    soul_qc = QuantumCircuit(players, is_alive)

    statevector = np.array(
        list(
            map(
                vector2_to_complex,
                [
                    soul["neitherLive"],
                    soul["rightyLives"],
                    soul["leftyLives"],
                    soul["bothLive"],
                ],
            )
        )
    )
    soul_qc.initialize(statevector / np.linalg.norm(statevector))

    if gate == "H":
        soul_qc.h(players[target])
    elif gate == "X":
        soul_qc.x(players[target])
    elif gate == "Y":
        soul_qc.y(players[target])
    elif gate == "Z":
        soul_qc.z(players[target])
    elif gate == "RX":
        soul_qc.rx(angle, players[target])

    statevector = execute(soul_qc, sim).result().get_statevector().data
    statevector = list(map(complex_to_vector2, statevector))
    return {
        "neitherLive": statevector[0],
        "rightyLives": statevector[1],
        "leftyLives": statevector[2],
        "bothLive": statevector[3],
    }
