var swiper = new Swiper (".mySwiper-1",{
    slidesPerView:1,
    spaceBetween:30,
    loop: true,
    pagination:{
        EEl:".swiper-pagination",
        clickable: true,
    },
    navigation:{
         nextEl:".swiper-button-next",
         prevEl:".swiper-button-prev",
    },
    autoplay:{
        delay:3000,
    },
    fadeEffect: {
        crossFade: true, // Efecto de desvanecimiento
      },
      speed: 2000, // Duración de la animación (1 segundo)
});
var swiper= new Swiper(".mySwiper-2",{
    slidesPerView:3,
    spacebetween:20,
    loop:true,
    loopFillGroupWithBlank:true,
    navigation:{
         nextEl:".swiper-button-next",
         prevEl:".swiper-button-prev",
    },
    breakpoints:{
        0:{
            slidesPerview:1,
        },
        520:{
            slidesPerView:2,
        },
        950:{
            slidesPerView:3,
        },
        1024:{
            slidesPerView:4,
        },
        
    },
    autoplay:{
        delay:3000,
    },
    fadeEffect: {
        crossFade: true, // Efecto de desvanecimiento
      },
      speed: 2000,
    
    
});

let tabInputs = document.querySelectorAll(".tabInput");
tabInputs.forEach(function(input){

    input.addEventListener('change',function(){
        let id = input.ariavalueMax;
        let thisSwiper =document.getElementById('swiper'+ id);
    thisSwiper.swiper.update();
    
   })
});