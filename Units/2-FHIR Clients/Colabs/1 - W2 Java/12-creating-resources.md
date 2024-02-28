# Create a Resource

Creating a new instance of a resource class requires following these steps:

![Recipe Icon](./images/recipe-icon.png)

## Recipe # J-6: Create a Resource Instance

### Step 1: Create and populate the resource instance
```java
// Create and populate the resource instance  
|ResourceClass| |MyResourceName|;  
{  
    |ResourceClass| |MyResourceName| = new |ResourceClass|();  
    |MyResourceName|.|setElementName|(|ElementValue|);  
    // ...  
    |MyResourceName|.|addElementName|().|setElementName|(|ElementValue|) 
    // Or      
    .|addElementName|()..;  
}
```

### Step 2: Invoke the create method on the server
```java
// Invoke the create method on the server  
MethodOutcome outcome = client.create()  
    .resource(|MyResourceName|)  
    .execute(); 
```

### Step 3: Process the server's response
```java
// Process the server's response 
if (outcome.getCreated()) {  
    IIdType id = outcome.getId();  
    System.out.println("Created resource, got ID: " + id);  
} else {  
    System.out.println("Error "+outcome.getOperationOutcome().toString());  
}  
```

**You need to replace:**

| Key          | Description  |
|--------------|--------------|
| `\|ResourceClass\|` | The name of the resource class. Example: Patient  |
| `\|setElementName\|`, `\|ElementValue\|` | For non-repeating elements: the name and value - data type valid - of the element to fill in the resource.                     |
| `\|addElementName\|`   | For repeating elements: the name of the element to add to the resource or element. Not only when they actually repeat, but IF the cardinality is 0/1...n. |
| `\|MyResourceName\|`   | The variable holding the instance you are creating.                                                                            |


![Example Icon](./images/example-icon.png)

### Example #J-5: Create a Practitioner Instance

**Note**: This example is too long to include here, so you can download it from the gist link provided above.

Practitioner Data:
- Dellacroix, Madeleine, Canada Practitioner # (http://canada.gov/cpn: 51922)
- Phone #: 613-555-0192
- Address: 3766 Papineau Avenue, Montreal, Quebec, H2K 4J5
- Email: qcpamxms9dq@groupbuff.com
- Specialty: Gynecologist (http://canada.gov/cpnq: OB/GYN)

## Micro Assignment #J-5: Create a new Patient resource

Patient data:
- Smith, Alan, born 06 May 1965, male, mrn: http://testpatient.id/mrn/99999999
