from typing import List

import requests


def get_vital_signs(patient_id: int, sorting: List[str] = ('date', 'code')) -> str:
    """
    Get patient vital signs
    :param patient_id: ID of a specific patient
    :type patient_id: int

    :param sorting: List of sorting criteria
    :type sorting: List[str]

    :return: Pipe delimited Vital Signs. Single vital per line
    """
    url: str = f'https://fhirserver.hl7fundamentals.org/fhir/Observation?category=vital-signs&patient={patient_id}'

    sort_params: str

    # if sorting values are not empty
    if not bool(sorting):
        sort_params = ','.join(sorting)
        url = f'{url}&_sort={sort_params}'

    print(f'Retrieving Vital-Signs: [{url}]')

    vital_signs_response = requests.get(url).json()

    print(vital_signs_response)

    return 'Not finished being implemented'