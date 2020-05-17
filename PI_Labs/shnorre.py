from hashlib import sha256
from random import randint


def hashThis(r, M):
    hash = sha256()
    hash.update(str(r).encode())
    hash.update(M.encode())
    return int(hash.hexdigest(), 16)


g = 2
q = 2695139
x = 32991
y = pow(g, x, q)

M = "This is the message"
k = randint(1, q - 1)
a = pow(g, k, q)
h = hashThis(a, M) % q
s = (k - (x * h)) % (q - 1)

## Verification

rv = (pow(g, s, q) * pow(y, h, q)) % q
hr = hashThis(rv, M) % q

print("h " + str(h) + " should equal hr " + str(hr))