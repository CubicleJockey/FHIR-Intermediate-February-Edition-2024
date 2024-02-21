# U01-6: US Core Patient Goals Display (C#)

## Description
The simple C# program [DaIoM3](https://dotnetfiddle.net/DaIoM3) retrieves all the allergies for a Patient and shows the display name, clinical status, and verification status. You can run it using [DotNetFiddle](https://dotnetfiddle.net/).

### Part I: Quiz

Write which are the mandatory elements defined in FHIR US CORE Goal as described here: http://hl7.org/fhir/us/core/StructureDefinition-us-core-goal.html **[5 Points]**

### Part II: Text Submission

1. Create a C# program to retrieve the patient's clinical goals (include **ALL** mandatory elements with the *exception* of the `subject` element) and return them separated by `|` (pipe). **The output of your program should be your submission [5 Points]**

2. Example / each line is a different Goal:
```
mandatory_element_1|…|mandatory_element_n|… \r\n
mandatory_element_2|…|mandatory_element_n|… \r\n
```

3. You need to use our server @  http://fhirserver.hl7fundamentals.org/fhir
   Patient Id is 12984

### Part III: Code - We will verify it

Copy and Paste code as submission.

>NOTE: We used NuGet package hl7.fhir.r4 v4.3.0

NuGet Package: [HL7.FHIR.R4](https://www.nuget.org/packages/Hl7.Fhir.R4)

Command: `dotnet add package Hl7.Fhir.R4 --version 4.3.0`



**C# Snippet Runner:** [LINQPAD](https://www.linqpad.net/)