from typing import List

import requests


def get_vital_signs(patient_id: str, sorting: List[str] = ('date', 'code')) -> str:
    """
    Get patient vital signs
    :param patient_id: ID of a specific patient
    :type patient_id: str

    :param sorting: List of sorting criteria
    :type sorting: List[str]

    Required Data:
        URL: https://hl7.org/fhir/us/core/StructureDefinition-us-core-vital-signs.html
        Required: Marked with Red S block
        Skip: Patient + Category - these are known as they are search criteria
        Format: status|code|effectiveDateTime|valueQuantity[value unit]|dataAbsentReason
        
    :return: Pipe delimited Vital Signs. Single vital per line
    """
    url: str = f'http://fhirserver.hl7fundamentals.org/fhir/Observation?category=vital-signs&patient={patient_id}'

    # if sorting values are not empty
    if len(sorting) > 0:
        sort_params = ','.join(sorting)
        print(f'Adding sorting criteria to vital-signs request. {sort_params}')
        url = f'{url}&_sort={sort_params}'

    print(f'Retrieving Vital-Signs: [{url}]\n')

    vital_signs_response = requests.get(url).json()

    entries = vital_signs_response['entry']

    patient_vitals = list()
    for entry in entries:
        resource = entry['resource']
        status = resource['status']
        code = resource['code']['text']
        effective_date_time = resource['effectiveDateTime']

        # get numerical values
        value_quantity = resource['valueQuantity']
        value = round(value_quantity['value'], 2)
        unit = value_quantity['unit']

        data_absent_reason = None
        if 'dataAbsentReason' in resource:
            data_absent_reason = resource['dataAbsentReason']

        current_vital = f'{status}|{code}|{effective_date_time}|{value} {unit}'
        if data_absent_reason is not None:
            current_vital = f'{current_vital}|{data_absent_reason['text']}'

        patient_vitals.append(current_vital)

    return '\r\n'.join(patient_vitals)


#Run Code
patient_id = 'X12984'
vitals = get_vital_signs(patient_id)

print(vitals)