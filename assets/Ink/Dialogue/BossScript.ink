VAR correct = false
VAR question_count = 0

-> main

=== main ===
Hey, you're finally here.
Lets get started now shall we ?
Answer my questions to prove your worthiness
-> question_loop

=== question_loop ===
~ question_count += 1
{
    - question_count == 1: -> question1
    - question_count == 2: -> question2
    - question_count == 3: -> question3
    - question_count == 4: -> question4
    - question_count == 5: -> question5
    - question_count == 6: -> question6
    - question_count == 7: -> question7
    - question_count == 8: -> question8
    - question_count == 9: -> question9
    - question_count == 10: -> question10
    - else: -> end
}

=== question1 ===
What can be considered as an integer ?
* [A whole number]
    ~ correct = true
* [A decimal]
    ~ correct = false
* [A true false question]
    ~ correct = false
* [A group of words]
    ~ correct = false
- -> question_result

=== question2 ===
What data type would you use to store a single letter?
* [An int]
    ~ correct = false
* [A bool]
    ~ correct = false
* [A char]
    ~ correct = true
* [A float]
    ~ correct = false
- -> question_result

=== question3 ===
Which data type is used to store whole numbers in C++?
* [A float]
    ~ correct = false
* [A char]
    ~ correct = false
* [A string]
    ~ correct = false
* [An int]
    ~ correct = true
- -> question_result

=== question4 ===
What data type would you use to store true or false values?
* [An int]
    ~ correct = false
* [A bool]
    ~ correct = true
* [A char]
    ~ correct = false
* [A float]
    ~ correct = false
- -> question_result

=== question5 ===
Which data type is used to store text or words?
* [An int]
    ~ correct = false
* [A bool]
    ~ correct = false
* [A string]
    ~ correct = true
* [A float]
    ~ correct = false
- -> question_result

=== question6 ===
What is the correct way to declare a variable named "age" to store a whole number?
* [float age]
    ~ correct = false
* [char age]
    ~ correct = false
* [string age]
    ~ correct = false
* [int age]
    ~ correct = true
- -> question_result

=== question7 ===
What data type would you use to store a person's height in meters?
* [An boolean]
    ~ correct = false
* [An char]
    ~ correct = false
* [A string]
    ~ correct = false
* [A float]
    ~ correct = true
- -> question_result

=== question8 ===
What data type would you use to store a person's name?
* [An string]
    ~ correct = true
* [An float]
    ~ correct = false
* [A int]
    ~ correct = false
* [A float]
    ~ correct = false
- -> question_result

=== question9 ===
Which data type takes up the least amount of memory?
* [A float]
    ~ correct = false
* [An int]
    ~ correct = false
* [A char]
    ~ correct = true
* [A double]
    ~ correct = false
- -> question_result

=== question10 ===
Which of the following is NOT a basic data type in C++?
* [An int]
    ~ correct = false
* [An array]
    ~ correct = true
* [A char]
    ~ correct = false
* [A double]
    ~ correct = false
- -> question_resultLAST

=== question_result ===
{correct:
    -> correct_response
- else:
    -> wrong_response
}

=== question_resultLAST ===
{correct:
    -> correctLAST
- else:
    -> wrongLAST
}

=== correct_response ===
Hmph, guess you got lucky.
Very well, next question.
-> question_loop

=== wrong_response ===
Weakling. Its a wonder how you got here.
Let's see how long will you survive.
-> question_loop

=== correctLAST ===
Hmph, guess you got lucky.
->end

=== wrongLAST ===
Hmph, you barely make it.

-> question_loop

=== end ===
Fine, you're good. I'll admit it. It is your win.
-> END