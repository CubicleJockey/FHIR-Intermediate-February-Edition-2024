# U01-4: Screening Protocal Data (Terminology)

## Description

Analyze the screening protocal for new athletes on pages `176 - 177` of the attached document [**SportScreeningPaper.pdf**](./files/SportsScreeningPaper.pdf).

For **labs, vitals and immunizations**, our application needs to provide all the items that can be downloaded automatically from an Argonaut or IPS Server.

Your job as the clinical interface consultant for the company is to resolve these issues.

1. **Section 0 - Profiles/Resources [3 Points]**
   <br/>
   Which Argonaut profile will you use for each category: 
    * Lab
    * Vitals
    * Immunizations

2. **Section 1 - Immunization [6 Points]**
   <br />
   Define **code/code system** for Immunization to download from the FHIR server for the vitals required by the paper: 
    * Measles, Mumps, and Rubella (combination vaccine)
    * Influenza
    * Typhoid
    * Hepatitis A and B
    * Yellow Fever vaccine.

3. **Section 2 - Vitals [7 Points]**
   <br />
   Define **code/code system** for Vital Signs to download from the FHIR server for the vitals required by the paper:
    * Blood Pressure
    * Heart Rate
    * Body Height
    * Body Weight
    * BMI (Body Mass Index)

4. **Section 3 - Labs [4 Points]**
   <br />
   Define **code/code system** to download from the FHIR server for the clinical/microbilogical results required by the paper.



   ### Helpful Resources:
    * [Terminology Systems](https://hl7.org/fhir/DSTU2/terminologies-systems.html)
      * [CVX Vaccine Administered](https://www2a.cdc.gov/vaccines/iis/iisstandards/vaccines.asp?rpt=cvx)
      * [CodeSystem CVX](https://terminology.hl7.org/5.1.0/CodeSystem-CVX.html)
      * [CVX ValueSet](https://hl7.org/fhir/R4/valueset-vaccine-code.html)
      * [Loinc](https://loinc.org/search/)
        * Requires free account sign up to use code search for quiz.