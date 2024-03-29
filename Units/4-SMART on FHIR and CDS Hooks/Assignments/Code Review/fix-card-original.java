if (price) {
    card.put("summary", " Cost: $500.41. Save $426.47 with a generic medication.");
    card.put("detail", "Last price update: Two years ago");
    card.put("indicator", "info");
    JSONArray sources = new JSONArray();
    JSONObject source = new JSONObject();
    source.put("label", " CMS Public Use Files");
    sources.put(source);
    card.put("sources", sources);
    JSONArray suggestions = new JSONArray();
    JSONObject sugg = new JSONObject();
    sugg.put("label", "Check price update");
    sugg.put("uuid", UUID.randomUUID());
    suggestions.put(sugg);
    cardsArray.put(card);
    break;
}