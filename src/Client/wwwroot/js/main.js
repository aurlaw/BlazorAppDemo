window.processFile = (dotNetObject, input) => {
    var doc = document.querySelector(input);
    console.log(input);
    console.log(doc);

    var file = doc.files[0];
    var reader = new FileReader();
    reader.onload = function(){
        console.log(reader);
        var dataURL = reader.result;

        var output = { name: file.name, type: file.type, size: file.size, dataURL: dataURL};
        dotNetObject.invokeMethodAsync('ProcessFileResult', JSON.stringify(output));
        console.log(output);
        console.log(dotNetObject);

    };
    reader.readAsDataURL(file);    

};