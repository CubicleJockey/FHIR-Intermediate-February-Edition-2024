# This unit's goals

This unit is about developing FHIR clients using reference libraries and connecting to Argonaut/IPS-compliant FHIR servers.

We will present the general concepts for all client libraries (how to perform common tasks in FHIR: connecting, accessing resources, etc.) and then describe how each library allows you to do each task.

So, this unit, in fact, contains three different subunits on how to use each library (Java, .NET, JavaScript).

These subunits have a set of micro-assignments. These assignments will not be graded, and the solutions will be posted along with the reading material. You can choose to try to solve them by yourself or just review and try the solutions.

## What you'll learn

After Unit 2, you will be able to:

- Describe the general structure of a FHIR client: connection, security, resource model. \[Both tracks\]
- Relate the content of the IGs to their related structures in selected FHIR computer language implementations (JavaScript, Java, C#) using reference libraries. \[Both tracks\]
- Produce a FHIR client capable of connecting to an Argonaut Data Query server, finding a patient, receiving a set of resources, and displaying them to the user. \[Track A\]
- Produce a FHIR client capable of connecting to an Argonaut Provider Directory server, finding providers for a given specialty next to the patient location, and displaying the details to the user. \[Track A\]
- Produce a FHIR client capable of connecting to a FHIR/IPS server, finding a Patient Summary, and displaying it to the user. \[Track B\]
- Produce a FHIR client capable of connecting to a vocabulary server to retrieve a set of values for selections or value translations. \[Both tracks\]