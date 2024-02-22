# MedicationRequest request issues

* FHIR JSON must start/end with `{}` not `[]`
* The `genereted` text didn't have a correct closing `</div>`
* Status and Intent were in the `generated` text but not in the actual response body.
  * Added `"status": "active"`
  * Added `"intent": "order"`
* `patient` property needed to be renamed to `subject`
* `prescriber` propety needed to be renamed to `requester`