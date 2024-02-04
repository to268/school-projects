/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{vue,html,js}"],
  daisyui: {
    themes: [
      {
        TwitterTheme: {
          "primary": "#0ea5e9",
          "secondary": "#7dd3fc",
          "accent": "#0284c7",
          "neutral": "#191D24",
          "base-100": "#2A303C",
          "info": "#3ABFF8",
          "success": "#36D399",
          "warning": "#FBBD23",
          "error": "#F87272",
        },
      },
    ],
  },
  plugins: [require("daisyui")],
}

