<script setup>

</script>
<template>
    <div class="w-full h-full  text-4xl text-emerald-700 justify-center flex">
        <h1 class="ml5 mt-72">
            <span class="text-wrapper shadow-md  bg-transparent  rounded">
                <!-- <span class="line line1"></span> -->
                <span class="letters letters-left">HealthCarePro &nbsp;</span>
                <span class="letters ampersand "><i class="bi bi-heart-pulse-fill"></i></span>
                <span class="letters letters-right">&nbsp;</span>
                <!-- <span class="line line2"></span> -->
            </span>
        </h1>
    </div>
</template>
<style>
.ml5 {
    position: relative;
    font-weight: 300;
    font-size: 4.5em;
    color: rgb(255, 255, 255)
}

.ml5 .text-wrapper {
    position: relative;
    display: inline-block;
    padding-top: 0.1em;
    padding-right: 0.05em;
    padding-bottom: 0.15em;
    line-height: 1em;
}

.ml5 .line {
    position: absolute;
    left: 0;
    top: 0;
    bottom: 0;
    margin: auto;
    height: 3px;
    width: 100%;
    background-color: rgb(4 120 87);
    transform-origin: 0.5 0;
}

.ml5 .ampersand {
    font-family: Baskerville, serif;
    font-style: italic;
    font-weight: 400;
    width: 1em;
    margin-right: -0.1em;
    margin-left: -0.1em;
}

.ml5 .letters {
    display: inline-block;
    opacity: 0;
}
</style>
<script>
export default {
    // Options du composant/vue
    mounted() {
        this.loadExternalScripts().then(() => {
            anime.timeline({ loop: false })
                .add({
                    targets: '.ml5 .line',
                    opacity: [0.5, 1],
                    scaleX: [0, 1],
                    easing: "easeInOutExpo",
                    duration: 700
                }).add({
                    targets: '.ml5 .line',
                    duration: 600,
                    easing: "easeOutExpo",
                    translateY: (el, i) => (-0.625 + 0.625 * 2 * i) + "em"
                }).add({
                    targets: '.ml5 .ampersand',
                    opacity: [0, 1],
                    scaleY: [0.5, 1],
                    easing: "easeOutExpo",
                    duration: 600,
                    offset: '-=600'
                }).add({
                    targets: '.ml5 .letters-left',
                    opacity: [0, 1],
                    translateX: ["0.5em", 0],
                    easing: "easeOutExpo",
                    duration: 600,
                    offset: '-=300'
                }).add({
                    targets: '.ml5 .letters-right',
                    opacity: [0, 1],
                    translateX: ["-0.5em", 0],
                    easing: "easeOutExpo",
                    duration: 600,
                    offset: '-=600'
                })
        });
    },
    methods: {
        loadExternalScripts() {
            // Chargement des scripts externes
            const scriptsToLoad = [
                "https://cdnjs.cloudflare.com/ajax/libs/animejs/2.0.2/anime.min.js",
            ];

            const stylesheetsToLoad = [
            ];

            const loadScriptPromises = scriptsToLoad.map(this.loadScript);
            const loadStylesheetPromises = stylesheetsToLoad.map(this.loadStylesheet);

            return Promise.all([...loadScriptPromises, ...loadStylesheetPromises]);
        },
        loadScript(src) {
            return new Promise((resolve, reject) => {
                const script = document.createElement("script");
                script.src = src;
                script.onload = resolve;
                script.onerror = reject;
                document.body.appendChild(script);
            });
        },
        loadStylesheet(href) {
            return new Promise((resolve, reject) => {
                const link = document.createElement("link");
                link.rel = "stylesheet";
                link.type = "text/css";
                link.href = href;
                link.onload = resolve;
                link.onerror = reject;
                document.head.appendChild(link);
            });
        },
        initializeTrumbowyg() {
            $("#description").trumbowyg();
        },
    },
};
</script>