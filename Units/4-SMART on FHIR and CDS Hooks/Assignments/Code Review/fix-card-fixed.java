//https://cds-hooks.org/specification/current/#card-attributes

if (price) {
    //CARD Object    
    card.put("summary", " Cost: $500.41. Save $426.47 with a generic medication.");
    card.put("detail", "Last price update: Two years ago");
    card.put("indicator", "info");
    
    //SOURCE Object
    JSONArray sources = new JSONArray();
    JSONObject source = new JSONObject();
    sources.put("label", " CMS Public Use Files");
    sources.put(source);
    card.put("source", sources); //The required field for SOURCE is source NOT sources. Typo was fixed.

    //SUGGESTION Object
    JSONArray suggestions = new JSONArray();
    JSONObject sugg = new JSONObject();
    sugg.put("label", "Check price update");
    sugg.put("uuid", UUID.randomUUID());
    suggestions.put(sugg);
    cardsArray.put(card);
    break;
}