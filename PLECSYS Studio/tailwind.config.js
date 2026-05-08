/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./wwwroot/**/*.html",      // archivos estáticos en wwwroot
        "./Pages/**/*.razor",       // páginas Blazor
        "./Components/**/*.razor",  // componentes Blazor
        "./Shared/**/*.razor"       // layouts y componentes compartidos
    ],
    safelist: [
        "translate-x-0", "-translate-x-full",
        "fixed", "inset-y-0", "left-0", "w-64", "h-full",
        "bg-white", "shadow-lg", "transform",
        "transition-transform", "duration-300", "ease-out", "ease-in",
        "z-50", "z-40", "bg-black/30", "opacity-100", "opacity-0", "pointer-events-none"
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}
