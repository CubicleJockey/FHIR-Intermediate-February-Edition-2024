# U01-3: US Core FHIR R4 Vital Signs (Python)

## Description

The following Python* console program retrieves all the Procedures for a given patient 
sorted by date and code, and returns the date, status, and description (fields delimited by | 
and resources delimited by \r\n) (This is just a simple example we provide, and you can 
use to understand what to do in Python for this specific assignment).
Our customer wants to feed into the sports app all the simple numerical vital signs from the 
electronic health record using US Core access.

### Part I: Quiz

Explain which is the FHIR resource and which are the mandatory elements for the US Core
Vital Signs Profile **[5 Points]**


The Vital Signs US Core profile is described here:
http://hl7.org/fhir/us/core/StructureDefinition-us-core-vital-signs.html

### Part II: Submission Text

Create a program in Python to retrieve only the patient’s numeric vital signs which has no 
components, sorted by date and code (include the result value rounded to 2 decimals and 
other mandatory elements) **[5 Points]**

Just like in our small program, you need to separate the elements using a pipe and issue a 
new line after each vital sign.

1. The order of the elements is the order in which they appear in the US Core Profile
   
2. ***DO NOT INCLUDE*** patient or category information, since they are our search parameters.

3. For numeric data, only include the value and the unit separated by spaces (eg.: ’18 g/l’)
   
4. For CodeableConcept elements, only include the text element.

The output of your program should be your submission 
Example / each line is a different Vital Sign: 

```
    mandatory_element_1|…|mandatory_element_n|… \r\n
    mandatory_element_2|…|mandatory_element_n|… \r\n
```    
You need to use our server @ http://fhirserver.hl7fundamentals.org/fhir /
Patient Id is 12984.

### Part III: Submit Your Code - *We Will Verify It*

You can run the provided program here: https://replit.com/@fhirinterm/FHIRSearchProcedures?v=1

Or download it from here: https://gist.github.com/diegokaminker/35282deafa39da46eb51028b373d8d66

You can use your own Python installation or these free Python sandboxes:
* https://replit.com
* https://trinket.io

