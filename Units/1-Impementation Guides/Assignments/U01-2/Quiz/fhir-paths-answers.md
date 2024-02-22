# U01-2: US Core FHIR R4/IPS Medication (Mapping)

| Required Data                                           | FHIR Path                                   |
|---------------------------------------------------------|---------------------------------------------|
| IPS - FHIRPath for get the Medication Code              | `medicationCodeableConcept.coding.code`     |
| IPS - FHIRPath for get the Medication Date              | `effectiveDateTime`                         |
| IPS - FHIRPath for get the Medication Name              | `medicationCodeableConcept.coding.display`  |
| IPS - FHIRPath for get the Medication Prescriber        | `informationSource.display`                 |
| IPS - FHIRPath for get the Medication Statement Status  | `status`                                    |
| US CORE - FHIRPath for get the Medication Code          | `medicationCodeableConcept.coding.code`     |
| US CORE - FHIRPath for get the Medication Date          | `authoredOn`                                |
| US CORE - FHIRPath for get the Medication Name          | `medicationCodeableConcept.coding.display`  |
| US CORE - FHIRPath for get the Prescriber Name          | `requester.display`                         |
| US CORE - FHIRPath for get the Medication Status        | `status`                                    |


## IPS
```json
{
  "resourceType": "MedicationStatement",
  "id": "207527",
  "meta": {
    "versionId": "1",
    "lastUpdated": "2020-08-25T15:51:33.182+00:00"
  },
  "text": {
    "status": "generated",
    "div": "<div xmlns=\"http://www.w3.org/1999/xhtml\"><p><b>Generated Narrative with Details</b></p><p><b>id</b>: eumfh-39-07-1</p><p><b>status</b>: active</p><p><b>medication</b>: <a href=\"Medication-eumfh-39-07-1.html\">simvastatin. Generated Summary: id: eumfh-39-07-1; <span title=\"Codes: {http://www.nlm.nih.gov/research/umls/rxnorm 757704}, {http://www.whocc.no/atc C10AA01}\">Fluspiral 50 mcg</span>; <span title=\"Codes: {http://www.nlm.nih.gov/research/umls/rxnorm 1294713}, {http://standardterms.edqm.eu 10219000}\">Disintegrating Oral Product</span></a></p><p><b>subject</b>: <a href=\"Patient-eumfh-39-07.html\">Alexander Heig (inject 39-07). Generated Summary: id: eumfh-39-07; 39-07, EUR01P0001; active; Alexander Heig ; gender: male; birthDate: 1957-01-01</a></p><p><b>effective</b>: Jan 1, 2014 12:00:00 AM --&gt; (ongoing)</p><p><b>dosage</b>: </p></div>"
  },
  "status": "active",
  "medicationCodeableConcept": {
    "coding": [
      {
        "system": "http://www.nlm.nih.gov/research/umls/rxnorm",
        "code": "582620",
        "display": "Nizatidine 15 MG/ML Oral Solution [Axid]"
      }
    ],
    "text": "Nizatidine 15 MG/ML Oral Solution [Axid]"
  },
  "subject": {
    "reference": "Patient/79709",
    "display": "Sample Patient"
  },
  "effectiveDateTime": "2020-01-04",
  "informationSource": {
    "reference": "Practitioner/81",
    "display": "Tennie Fok, MD"
  },
  "dosage": [
    {
      "text": "40 mg/day",
      "timing": {
        "repeat": {
          "frequency": 1,
          "period": 1,
          "periodUnit": "d"
        }
      },
      "doseAndRate": [
        {
          "doseQuantity": {
            "value": 40,
            "unit": "mg",
            "system": "http://unitsofmeasure.org",
            "code": "mg"
          }
        }
      ]
    }
  ]
}
```

## US Core R4
```json
{
  "resourceType" : "MedicationRequest",
  "id" : "uscore-mo1",
  "meta" : {
    "profile" : [
      "http://hl7.org/fhir/us/core/StructureDefinition/us-core-medicationrequest"
    ]
  },
  "text" : {
    "status" : "generated",
    "div" : "<div xmlns=\"http://www.w3.org/1999/xhtml\"><p><b>Generated Narrative with Details</b></p><p><b>id</b>: uscore-mo1</p><p><b>meta</b>: </p><p><b>status</b>: active</p><p><b>intent</b>: order</p><p><b>medication</b>: Nizatidine 15 MG/ML Oral Solution [Axid] <span style=\"background: LightGoldenRodYellow\">(Details : {RxNorm code '582620' = 'Nizatidine 15 MG/ML Oral Solution [Axid]', given as 'Nizatidine 15 MG/ML Oral Solution [Axid]'})</span></p><p><b>subject</b>: <a href=\"Patient-example.html\">Amy Shaw. Generated Summary: id: example; Medical Record Number = 1032702 (USUAL); active; Amy V. Shaw ; ph: 555-555-5555(HOME), amy.shaw@example.com; gender: female; birthDate: 2007-02-20</a></p><p><b>authoredOn</b>: 05/04/2008 12:00:00 AM</p><p><b>requester</b>: <a href=\"Practitioner-practitioner-1.html\">Ronald Bone, MD. Generated Summary: id: practitioner-1; 9941339108, 25456; Ronald Bone </a></p><p><b>dosageInstruction</b>: </p><h3>DispenseRequests</h3><table class=\"grid\"><tr><td>-</td><td><b>NumberOfRepeatsAllowed</b></td><td><b>Quantity</b></td><td><b>ExpectedSupplyDuration</b></td></tr><tr><td>*</td><td>1</td><td>480 mL<span style=\"background: LightGoldenRodYellow\"> (Details: UCUM code mL = 'mL')</span></td><td>30 days<span style=\"background: LightGoldenRodYellow\"> (Details: UCUM code d = 'd')</span></td></tr></table></div>"
  },
  "status" : "active",
  "intent" : "order",
  "medicationCodeableConcept" : {
    "coding" : [
      {
        "system" : "http://www.nlm.nih.gov/research/umls/rxnorm",
        "code" : "582620",
        "display" : "Nizatidine 15 MG/ML Oral Solution [Axid]"
      }
    ],
    "text" : "Nizatidine 15 MG/ML Oral Solution [Axid]"
  },
  "subject" : {
    "reference" : "Patient/example",
    "display" : "Amy Shaw"
  },
  "authoredOn" : "2008-04-05",
  "requester" : {
    "reference" : "Practitioner/practitioner-1",
    "display" : "Ronald Bone, MD"
  },
  "dosageInstruction" : [
    {
      "text" : "10 mL bid",
      "timing" : {
        "repeat" : {
          "boundsPeriod" : {
            "start" : "2008-04-05"
          }
        }
      }
    }
  ],
  "dispenseRequest" : {
    "numberOfRepeatsAllowed" : 1,
    "quantity" : {
      "value" : 480,
      "unit" : "mL",
      "system" : "http://unitsofmeasure.org",
      "code" : "mL"
    },
    "expectedSupplyDuration" : {
      "value" : 30,
      "unit" : "days",
      "system" : "http://unitsofmeasure.org",
      "code" : "d"
    }
  }
}
```
