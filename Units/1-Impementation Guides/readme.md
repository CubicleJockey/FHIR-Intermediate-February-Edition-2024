# Implementation Guides

>This Unit discusses the most globally relevant implementation guides (IG) currently in use: Argonaut/US CORE and International Patient Summary.


## This Week's Assignment

Since this is the first week, we will not have extensive programming assignments, but we will 
provide you with some small [*Argonaut*](https://www.hl7.org/implement/standards/fhir/2015Jan/argonauts.tml) and [*IPS*](https://www.hl7.org/fhir/uv/ips/) example programs in C#, Python and JavaScript so 
you can get used to access FHIR data using FHIR client or plain http access libraries. 

It’s a good time to set up your environment. Get a good code editor, like [*Sublime*](https://www.ublimetext.com/download) or [*Visual Studio Code*](https://code.visualstudio.com/) (highly recommended for ll the platforms - even Java! -, VS Code works in Windows /
MacOS / Linux.

Make sure you can run JavaScript, Java or C#, whatever you prefer.
For this unit, there is a quiz and a first set of assignments. 
You will have to review or write small fragments of code, select IGs/resources and answer some 
questions on what you’ve learned about Argonaut and IPS in this unit.

## Unit Summary and Conclusion

[FHIR](https://www.hl7.org/fhir/) is a “platform” specification. To achieve real interoperability, we need to constrain. 

And constraints are ways to express community consensus on how to use the standard for realworld problems, use cases and scenarios. We explored two implementations guides in this unit: 
Argonaut/US CORE and IPS. They are the result of months (years!) of discussions and are being 
widely implemented.

But the requirements are different, so the resulting interoperability artifacts and even the methods
for exchange (granular API search vs. bundled document) are different.

We hope this unit helps you understand that even when you can create a FHIR client or server 
(native of facade) for a specific goal or compliant with a specific IG, it doesn’t mean that it will  automatically be good for another use case/scenario or implementation guide.
This may be seen as a weakness for FHIR, but we think just the opposite: FHIR is powerful, and 
you need to use this power for your specific goals satisfying your stakeholders’ needs.