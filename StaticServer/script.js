const form = document.getElementById('form');
form.addEventListener('submit', async function(event){
    event.preventDefault();

    let tag = {
        messasge: form.message,
        font: form.elements.font
    };

    let response = await fetch('https://5.150.234.187/tag', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(tag)
    });

    let result = await response.json();

    console.log(response);
    console.log(body);

    alert("inskickad");

    location.reload(true);

    return false;
});

(async function(){
    const parent = document.getElementById("main");
    
    let response = await fetch('https://5.150.234.187/tags');
    let result = await response.json();

    let array = JSON.parse(result);
    array.forEach(tag => {
         let p = document.createElement("p");

         p.innerHTML = tag.message;
         p.style.rotation = tag.rotation + "deg";
         p.style.font = "30px " + tag.font;
         
        parent.appendChild(p);
    });
})();