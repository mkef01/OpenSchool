(function() {

    const examenDiv = document.getElementById("examen");
    const resultadosDiv = document.getElementById("resultados");
    const enviar = document.getElementById("enviar");

    const anterior = document.getElementById("anterior");
    const siguiente = document.getElementById("siguiente");
    const slides = document.querySelectorAll(".slide");
    let currentSlide = 0;

    showSlide(0);

    anterior.addEventListener("click", showPreviousSlide);
    siguiente.addEventListener("click", showNextSlide);

    function showSlide(n) {
        slides[currentSlide].classList.remove("active-slide");
        slides[n].classList.add("active-slide");
        currentSlide = n;

        if (currentSlide === 0) {
            anterior.style.display = "none";
        } else {
            anterior.style.display = "inline-block";
        }

        if (currentSlide === slides.length - 1) {
            siguiente.style.display = "none";
            enviar.style.display = "inline-block";
        } else {
            siguiente.style.display = "inline-block";
            enviar.style.display = "none";
        }
    }

    function showNextSlide() {
        showSlide(currentSlide + 1);
    }

    function showPreviousSlide() {
        showSlide(currentSlide - 1);
    }

})();