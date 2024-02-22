# U01-7: US Core FHIR R4 Implantable Device (C#)

## Description

Our company closed a deal with the site Cardiac Athletes and is providing support to recovering cardiac patients on running teams. So we have added support for Implantable Devices.

The C# code provided with this assignment, located at [https://dotnetfiddle.net/EAuZTx](https://dotnetfiddle.net/EAuZTx) should search for all the US CORE FHIR R4 Implantable Devices resources for an athlete on the Cardiac Athletes team and display all mandatory/must-support elements.

In case you do not remember where to find the specs, the spec for US CORE FHIR R4 is here: [https://www.hl7.org/fhir/us/core/StructureDefinition-us-core-implantable-device.html](https://www.hl7.org/fhir/us/core/StructureDefinition-us-core-implantable-device.html)

### Part I: Quiz - Review the C# code and answer:

1. Why does this code fail in searching the Implantable Devices for an athelete? **[2 Points]** 
2. Which mandatory/must-support elements is the program not showing? **[3 Points]**

### Part II: Text Submission

Correct the C# code to retreive the mandatory/must support elements **[5 Points]**.

You need to return the mandatory/must-support elements for each resource related to the patient, in the order they appear in the Device resource definition, separated by `|` (pipe).

The output of your program should be your submission.

> NOTE: For CodableConcept elements, only include `display` element.

Example: each line is a different Device Resource:

```
mandatory_ms_element_1|…|mandatory_ms_element_n|… \r\n
mandatory_ms_element_2|…|mandatory_ms_element_n|… \r\n
```

You need to use our server @ [http://fhirserver.hl7fundamentals.org/fhir](http://fhirserver.hl7fundamentals.org/fhir)

**Patiend ID:** `1`

### Part III: Submit Your Code - We Will Verify It