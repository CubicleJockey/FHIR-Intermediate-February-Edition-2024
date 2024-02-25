# Vignette Questions

## VIGNETTE 1

* **Quesiton:** If the endpoint for the server is http://serverfhir.org/r4, enter the full url to obtain all the patient's allergies
* **Answer:**
  * Resources:
    * https://hl7.org/fhir/allergyintolerance.html
  * URL: - [ ] http://serverfhir.org/r4/AllergyIntolerance?patient=[PATIENT_ID]

## VIGNETTE 1

 * **Question:** If the endpoint for the FHIR server is http://pcpfhir.org/r4, enter the full url to query all the patient's information. 
 * **Answer:** 
   * Resources: 
     * https://hl7.org/fhir/patient.html
   * URL: - [x] http://pcpfhir.org/r4/Patient?name=[PATIENT_NAME] 

## VIGNETTE 2

 * **Question:** If the endpoint for the FHIR server is http://providersfhir.org/r4, enter the full url to query for providers. 
 * **Answer:**
   * Resources:
     * https://hl7.org/fhir/practitioner.html
   * URL: - [x] http://providersfhir.org/r4/Practitioner?name=[PRACTIONER_NAME] 

## VIGNETTE 2

 * **Question:** If the endpoint for the FHIR server is http://providersfhir.org/r4, enter the full url to find a patient. 
 * **Answer:**
   * Resources:
     * https://hl7.org/fhir/patient.html
   * URL: - [x] http://providersfhir.org/r4/Patient?name=[PATIENT_NAME]

## VIGNETTE 2

 * **Question:** If the endpoint for the FHIR server is http://resultsfhir.org/r4, enter the full url to query for documents. 
 * **Answer:**
   * Resources:
     * https://hl7.org/fhir/documentreference.html
   * URL: - [ ] http://resultsfhir.org/r4/DocumentReference?patient=[PATIENT_ID]

## VIGNETTE 3

 * **Question:** If the endpoint for the FHIR server is http://medicarefhir.org/r4, enter the full url to query and retrieve the highest risk patients based on diabetes control levels, and reach out to those who should be screened for early signs of diabetic retinopathy.
 * **Answer:**
   * Resources:
     * Coding Systems:
       * [Snomed](http://snomed.info/sct)
       * [ICD 10](http://hl7.org/fhir/sid/icd-10)
   * URL: - [ ] http://medicarefhir.org/r4/Condition?code=http://snomed.info/sct|73211009&severity:text=severe

## VIGNETTE 3

 * **Question:** If the endpoint for the FHIR server is http://medicarefhir.org/r4, enter the full url to query and retrieve last known A1C. 
 * **Answers:**
   * Resources:
     * [Hemoglobin A1c/Hemoglobin.total in Blood](https://loinc.org/search/?t=1&s=4548-4)
   * URL: - [] http://medicarefhir.org/Observation?code=http://loinc.org|4548-4&subject=Patient/[patient_id]&_sort=-date

## VIGNETTE 3

 * **Question:** If the endpoint for the FHIR server is http://medicarefhir.org/r4, enter the full url to query the date of the last eye exam for the panel of patients identified per the quality measure specification
 * **Answer:**