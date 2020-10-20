
$(document).ready(function()
{
    $('.nav ul li').click(function() {
        $(this).addClass("active").siblings().removeClass("active");
    
    })

});



const tabbtn=document.querySelectorAll('.side-nav ul li');
const tab=document.querySelectorAll('.side-tab');



function tabs(panelIndex)
{
    tab.forEach(function(node)
    {
        node.style.display='none';

    });

    tab[panelIndex].style.display="block";
}

tabs(1);






let bio = document.querySelector('.bio');

function bioText()
{
    bio.innerText=bio.innerText.substring(0,100) + "...";
}

bioText();



