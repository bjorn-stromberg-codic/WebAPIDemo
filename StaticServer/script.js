const server = 'https://5.150.234.187';

(async function(){
    const parent = document.getElementById('main');
    
    const response = await fetch(server + '/tags');
    const result = await response.json();

    result.forEach(tag => {
         const p = document.createElement("p");

         p.innerHTML = tag.message;
         p.style.rotation = tag.rotation + 'deg';
         p.style.fontFamily = tag.font;

        parent.appendChild(p);
    });
})();

const form = document.getElementById('form');
form.addEventListener('submit', async function(event){
    event.preventDefault();

    const tag = {
        message: form.elements.message.value,
        font: form.elements.font.value
    };

    const response = await fetch(server + '/tag', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(tag)
    });

    const result = await response.json();

    console.log(response);
    console.log(result);

    location.reload(true);

    return false;
});