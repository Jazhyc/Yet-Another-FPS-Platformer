import nltk
import sys

TERMINALS = """
Adj -> "country" | "dreadful" | "enigmatical" | "little" | "moist" | "red"
Adv -> "down" | "here" | "never"
Conj -> "and" | "until"
Det -> "a" | "an" | "his" | "my" | "the"
N -> "armchair" | "companion" | "day" | "door" | "hand" | "he" | "himself"
N -> "holmes" | "home" | "i" | "mess" | "paint" | "palm" | "pipe" | "she"
N -> "smile" | "thursday" | "walk" | "we" | "word"
P -> "at" | "before" | "in" | "of" | "on" | "to"
V -> "arrived" | "came" | "chuckled" | "had" | "lit" | "said" | "sat"
V -> "smiled" | "tell" | "were"
NP -> N | RAdj N | Det N | Det RAdj N
RAdj -> Adj | Adj RAdj
PP -> P
VP ->  V | V NP | V  NP | QVP | QVP  NP
QVP -> Adv V | V Adv
CL -> NP VP | NP
PoP -> PP NP | PP NP PoP
"""
#VP - Verb Phrase
#PP - Proposition Phrase
#CL - Clause (Contains subject and maybe object)
#PoP - Gives position of objects (Objects' position can be based on other objects)
#QVP - Gives quality of actions
#RAdj - Recursive adjective (Preserves Sanity in Noun chunks)

NONTERMINALS = """

S -> CL | CL PoP | CL PoP Adv 
S -> S Conj S | CL Conj VP | CL PoP Conj VP PoP
"""

grammar = nltk.CFG.fromstring(NONTERMINALS + TERMINALS)
parser = nltk.ChartParser(grammar)


def main():

    # If filename specified, read sentence from file
    if len(sys.argv) == 2:
        with open(sys.argv[1]) as f:
            s = f.read()

    # Otherwise, get sentence as input
    else:
        s = input("Sentence: ")

    # Convert input into list of words
    s = preprocess(s)

    # Attempt to parse sentence
    try:
        trees = list(parser.parse(s))
    except ValueError as e:
        print(e)
        return
    if not trees:
        print("Could not parse sentence.")
        return

    # Print each tree with noun phrase chunks
    for tree in trees:
        tree.pretty_print()

        print("Noun Phrase Chunks")
        for np in np_chunk(tree):
            print(" ".join(np.flatten()))


def preprocess(sentence):
    """
    Convert `sentence` to a list of its words.
    Pre-process sentence by converting all characters to lowercase
    and removing any word that does not contain at least one alphabetic
    character.
    """
    # Separates words, then iterates over them, checks for alphabets
    words = nltk.word_tokenize(sentence)
    pwords = [] # Processed Words

    for word in words:
        has_letter = False
        pword = ''
        for i in range(len(word)):

            if word[i].isalpha():
                has_letter = True
                pword += word[i].lower()

        if has_letter == True:
            pwords.append(pword)

    return pwords


def np_chunk(tree):
    """
    Return a list of all noun phrase chunks in the sentence tree.
    A noun phrase chunk is defined as any subtree of the sentence
    whose label is "NP" that does not itself contain any other
    noun phrases as subtrees.
    """
    chunks = []
    for i in tree.subtrees():
        if i.label() == 'NP':
            if 'NP' not in i.subtrees():
                chunks.append(i)
    return chunks


if __name__ == "__main__":
    main()
