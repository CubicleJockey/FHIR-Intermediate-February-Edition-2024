## Populating a Patient Resource

Let's put together these two resources (Patient and Observation) to demonstrate how to use the most common data types in Java HAPI FHIR. We will try to use the fluent style whenever possible – chaining methods using periods, as in `Object.Method1().Method2()`.

### Demographic Info about Our Patient:

> **Name:**
>  [use] Official [Prefix] Ms. [Given] Eve [Family] Everywoman, [Suffix] III
>
> **Photo:** Base64 data >`iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAC0lEQVR4nGNgAAIAAAUAA>Xpeqz8=`
>
> **Identifiers:**
>  - System: http://hospital.gov/patients, Value: 9999999 (Medical >Record Number)
>  - System: http://citizens-id.gov/citizens, Value: 69999999-I >(National Identifier)
>
>**Address:** 9999 Patient Street, Ann Arbor, MI (90210), USA
>
> **Phone #:** (777) 555-9999
>
> **Email Address:** eve@everywoman.com
>
> **Gender:** Female
>
> **Active:** No
>
> **Deceased on:** Feb 13, 2019 10:30:00
>
> **Marital Status:** Widow
>
> **Born on:** July 23, 1968
>
> **Preferred Language:** English (USA). Also speaks Spanish
>
> **Organization in Charge:** Ann Arbor General Hospital (www.aagh.>org) – 9999 General Hospital Street, Ann Arbor, MI (90210), USA
>
> **Observation:** Lab – Fasting Serum Glucose Value: 6,3 mmol/L, Jan >20 20 07:00:00 EST / LOINC Code: 14771-0 (http://loinc.org)

![Example Icon](./images/example-icon.png)

### Example #J-12: Populate Patient and Observation

> Note:  The full example is too long to include here, so you can download it from the gist link above.

**Person Names (HumanName):**
```java
p=new Patient();  
p.addName().addGiven("Eve").setFamily("Everywoman")
   .addSuffix("III").addPrefix("Ms.").setUse(HumanName.NameUse.OFFICIAL);
```

**Address (Address)**
```java
p=new Patient(); 
Address a=new Address();  
a.addLine("9999 Patient Street").setCity("Ann Arbor")
 .setCountry("US").setState("MI").setPostalCode("90210");  
p.addAddress(a);
```

**Dates (Date Types)**
```java
DateType i = new DateType();  
i.fromStringValue("1968-07-23"); 
p.setBirthDateElement(i);
```

**Date/Time/Instant**
```java
DateTimeType DeceaseTime;  
DeceaseTime=DateTimeType.parseV3("20190213103000");  
p.setDeceased(DeceaseTime);
```

**Identifiers (identifier)**
```java
p.addIdentifier(new Identifier()
.setSystem("http://citizens-id.gov/citizens")  
.setValue("69999999-I")  
.setUse(Identifier.IdentifierUse.OFFICIAL));
```

**Attachments (attachment)**
```java
pho=new Attachment();  
//String to Byte 
String s="iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAC0lEQVR4nGNgAAIAAAUAAXpeqz8=";  
byte[] b =s.getBytes(Charset.forName("UTF-8"));  
pho.setData(b);  
pho.setContentType("image/png");  
p.addPhoto(pho);
```

**Coded Values (Codable Concepts)/Simple Codes - (Enumeration)**
```java
//Communication: Languages Codeable Concept + preferred flag  
Patient.PatientCommunicationComponent c1;  
CodeableConcept la1;  
la1=new CodeableConcept();  
la1.addCoding().setSystem("urn:ietf:bcp:47").setCode("en-US"); 
CodeableConcept la2;  
la2=new CodeableConcept(); 
la2.addCoding().setSystem("urn:ietf:bcp:47").setCode("es");  
p.addCommunication().setLanguage(la1).setPreferred(true);  
p.addCommunication().setLanguage(la2).setPreferred(false);  
//Marital Status - Codeable Concept  
CodeableConcept ms;  
ms=new CodeableConcept();  
ms.addCoding()  
.setSystem("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus")  
.setCode("W");  
p.setMaritalStatus(ms);
```

**Email and Telephone Numbers (telecom)**
```java
//Phone  
p.addTelecom()  
    .setSystem(ContactPoint.ContactPointSystem.PHONE)  
    .setValue("(777) 555-9999");  
//E-Mail Address  
p.addTelecom()  
    .setSystem(ContactPoint.ContactPointSystem.EMAIL)  
    .setValue("eve@everywoman.com");
```

**Active (boolean)**
```java
p.setActive(false);
```

**Quantities (quantity)**
```java
Quantity q=new Quantity();  
q.setValue(6.3).setUnit("mmol/L") 
 .setCode("mmol/L")  
 .setSystem("http://unitsofmeasure.org");
```

**Reference to a Local Resource (reference)**
```java
IIdType id;  
id=outcome.getId();  
Reference r = new Reference();  
String display=p.getName().get(0).getFamily().toString()  
 +" , "+p.getName().get(0).getGivenAsSingleString();  
r.setDisplay(display);  
r.setReference(id.getResourceType()+"/"+ id.getIdPart());  
obs.setSubject(r);
```

**Full Reference to a Logical Resource Id (reference)**
```java
IIdType id;  
id=outcome.getId();  
Reference r = new Reference();  
String display=p.getName().get(0).getFamily().toString()  
 +" , "+p.getName().get(0).getGivenAsSingleString();  
r.setDisplay(display);  
r.setReference(id.getValue());  
obs.setSubject(r);
```

**Narrative**
```java
Narrative n=new Narrative(); 
n.setStatusAsString("generated");  
n.setDivAsString("PLAIN TEXT FOR THE NARRATIVE");  
resource.setText(n)
```

**Approach #1:** Manually completing the narrative ("text" element) of the resource.

Manually completing the narrative (plain text or html):

```java
Narrative n=new Narrative(); 
n.setStatusAsString("generated");  
n.setDivAsString("PLAIN TEXT FOR THE NARRATIVE");  
resource.setText(n);
```

**Approach #2:** Leveraging automatic narrative completion templates.

HAPI FHIR will create narrative using Thymeleaf templates.

NOTE: The HAPI FHIR site states that this feature is a work in progress: Not all resources have templates, and they do not cover all the elements for each resource.

To use these features:

1. Include these two lines after initializing your context (ctx)
```java
INarrativeGenerator narrativeGen = new DefaultThymeleafNarrativeGenerator(); 
ctx.setNarrativeGenerator(narrativeGen);
```
2. Include these two imports
```java
import ca.uhn.fhir.narrative.DefaultThymeleafNarrativeGenerator;  
import ca.uhn.fhir.narrative.INarrativeGenerator;  
```
3. Include these two dependencies (Thymeleaf and Caffeine) in your pom.xml file for template management
```xml
<dependency>
   <groupId>com.github.ben-manes.caffeine</groupId>
   <artifactId>caffeine</artifactId>
   <version>2.5.5</version>
</dependency>
 <dependency>
   <groupId>org.thymeleaf</groupId>
   <artifactId>thymeleaf</artifactId>
   <version>3.0.0.RELEASE</version>
 </dependency>
```

More details on this approach can be found here (i.e. how to create your own templates): https://hapifhir.io/hapi-fhir/docs/model/narrative_generation.html

![Micro-Assignment Icon](./images/micro-assignment-icon.png)

## Micro Assignment #J-15: Use FHIR R4 Data Types

Create a program to create a patient resource and its related lab result resource with the information provided below:
   
>Name:
>
>[use] Official [Prefix] Mr. [Given] Adam [Family] Alvarado, [Suffix] >II
>
>**Photo: data (base64):**
>iVBORw0KGgoBBBBNSUhEUgBBBBEBBBBBCAYBBBBfFcSJBBBBC0lEQVR4nGNgBBIBBBUBBX>peqz8=
>
>Identifiers
>
>[system] http://nygc.com/patients [value] 1234567 (Medical Record >Number)
Z>
>[system] http://citizens-id.gov/citizens [value] 59999999-I (National >Identifier)
>
>**Address:** 1234 Elm Street, New York, NY (90210), USA
>
>**Phone #:** (555) 777-9999
>
>**e-mail Address:** alvarado@everymail.com
>
>Gender: Male
>
>Active: Yes
>
>Marital Status: Married
>
>**Born on:** May 20, 1978
>
>**Preferred Language:** Spanish (Spain). Also speaks English
>
>**Organization in Charge:** New York General Clinic (www.nygc.com) – 9999 >General Clinic Avenue, New York, NY (90210), USA NPI-ID (http://npi.>org/identifiers): 7777777
>
>**Observation:** Lab –Serum Creatinine Value: 65 umol/L, March 3, 2020 >07:00:00 EST / LOINC Code: 14682-9 (http://loinc.org)