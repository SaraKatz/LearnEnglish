function test() {
    //var b = Xrm.Page.getAttribute("telephone1").getValue();
    
    //var a = Xrm.Page.getAttribute("mobilephone").getValue()
    //var tmp;
    
    //tmp = a;
    //a = b;
    //b = tmp;
    // alert(name + id +  dirty);
  var fetch1=  "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>" +
"  <entity name='lead'>" +
"    <attribute name='telephone1' />" +
"    <attribute name='leadid' />" +
"    <order attribute='fullname' descending='false' />" +
"    <filter type='and'>" +
"      <condition attribute='telephone1' operator='not-null' />" +
"    </filter>" +
"  </entity>" +
"</fetch>"

  var entities = XrmServiceToolkit.Soap.Fetch(fetch1);
  var entities = entities[0].attributes.telephone1.value();
}