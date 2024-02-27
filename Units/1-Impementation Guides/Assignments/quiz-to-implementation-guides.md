# Quiz To Implementation Guides 
> Answers Bolded

## Question 1
Why do we state that FHIR is a “platform specification”?

Select one:
- **a. Because it requires adaptation to suit a particular context of use Correct**
- b. Because it defines how an API for a complete medical platform should behave
- c. Because it requires a RESTful resource-enabled platform to function
- d. Because it’s an umbrella for a set of Java/JavaScript and Python API specs

## Question 2
What is the primary goal of the Argonaut Data Query/US Core IGs?

Select one:
- a. Allow physicians to create clinical notes from their phones without using their EHR software
- **b. Allow physicians/patients to safely access demographic/clinical information perfect**
- c. Allow patients to connect their personal wearable devices to the EHR.
- d. Create a nationwide network of healthcare information so anyone can read/write its own clinical information

## Question 3
Which spec is the basis for Argonaut Smart-On-FHIR authentication? 

Select one:
- a. OCF Security Specification (Open Connectivity Forum)
- b. ISO 28000: 2007 – Specification for Security Management
- **c. oAuth** 
- d. ws-security specification 1.1

## Question 4
Which guide we discussed allows the specific FHIR version to be stated in the exchanged resources? 

Select one:
- **a. Argonaut Clinical Notes**
- b. Argonaut Provider Directory
- c. Argonaut Scheduling
- d. CDS-Hooks 

## Question 5
What is the meaning of that red letter S in the tabular versions of profiles in an implementation guide  

Select one:
- **a. ‘Must-Support’: as a server, if you have this information in your system, you are mandated to send it. As a client, you are supposed to process this information, display it to your users, or, at least not fail when receiving them.**
- b. ‘Must-Support’: its inclusion is mandatory if you are a server. Processing is mandatory if you receive the information as a client.
- c. ‘Summary’: You need to include the information if the client requests the resource with the _summary keyword
- d. ‘Mandatory-Server’: its inclusion is mandatory if you are a server. Client Processing is optional

## Question 6
Which are the mandatory elements for a conformant FHIR US-Core Patient resource instance?

Select one:
- **a. Name, gender, identifier** 
- b. Name, gender, identifier, birth-date
- c. Name, identifier, birth-date
- d. Identifier, gender, birth-date

## Question 7
Which extensions are defined for the FHIR US-Core Patient profile?

Select one:
- a. For Preferred Care Provider
- b. For Preferred Contact Name
- **c. For Race, Ethnicity,  Birth Sex, and Gender Identity** 
- d. For Preferred Phone

## Question 8
Which resources are constrained by the Provider Directory IG? Select the most complete list.

Select one:
- a. Practitioner, PractitionerRole, Location
- b. Practitioner, PractitionerRole, Organization, Patient, AllergyIntolerance
- c. Practitioner, PractitionerRole, Location, Organization
- **d. Practitioner, PractitionerRole, Location, Organization, Endpoint** 
