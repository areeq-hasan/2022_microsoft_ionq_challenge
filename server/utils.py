def vector2_to_complex(vec):
    return vec["x"] + vec["y"] * 1j


def complex_to_vector2(complex):
    return {"x": complex.real, "y": complex.imag}
