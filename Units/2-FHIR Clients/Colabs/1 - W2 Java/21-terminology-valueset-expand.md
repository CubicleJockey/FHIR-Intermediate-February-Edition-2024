## Terminology: ValueSet / $expand
This may be the most useful and powerful terminology operation for mainstream clients: You can use this operation to populate a combo/list box or enable smart completion for a text box, and also to do clinical decision support.

To parse the response, you need to traverse all the `ValueSet.=> Expansion => Contains` elements of the response.

![Receipe Icon](./images/recipe-icon.png)

### Recipe #J-12: Invoking ValueSet / $expand
```java
try {  
   Parameters outParams = client  
      .operation()  
      .onType(ValueSet.class)  
      .named("expand")  
      .withParameter(Parameters.class, "url",   
          new UriType("|url|"))  
      .andParameter("filter", new StringType("|filter|"))  
      .execute();  
   ValueSet v = (ValueSet)outParams.getParameter().get(0).getResource();  
   ValueSet.ValueSetExpansionComponent  
   ve = v.getExpansion();  
   for (int i = 0; i < ve.getTotal(); i++) {             
      ValueSet.ValueSetExpansionContainsComponent  
      ci = ve.getContains().get(i);  
      String code = ci.getCode();  
      String display = ci.getDisplay();  
      System.out.println(code + ":" + display);  
   }  
} catch (Exception e) { 
   System.out.println("Error:" + e.getMessage());  
}  
```
You need to replace:

- `|url|`: url to define which reference set or "domain" you are searching in the terminology server.

  The general syntax for url in SNOMED CT is:

  - `http://snomed.info/sct?fhir_vs=ecl/^|parameter|`
  
  or
  
  - `http://snomed.info/sct?fhir_vs=isa/|parameter|`

  - ecl: Expression Constraint Language, so you can replace `|parameter|` with any ecl expression.
  
  - isa: is-a relationship, so the query will return all concepts with an is-a relationship with the `|parameter|`.

  For more details see also [https://www.hl7.org/fhir/snomedct.html](https://www.hl7.org/fhir/snomedct.html)

- `|filter|`: Further filtering of the concepts.

![Micro-Assignment](./images/micro-assignment-icon.png)

### Micro Assignment #J-12: ValueSet Expansion
- Search for all the concepts related to diabetes – 73211009 – (relationship: is-a)
- Search for all the concepts in the General Practice Reference Set (450970008). Extra filter: pain.
