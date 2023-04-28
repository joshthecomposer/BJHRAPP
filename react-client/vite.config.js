import { defineConfig } from 'vite'
import { fileURLToPath } from 'node:url'
import reactRefresh from '@vitejs/plugin-react-refresh'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [reactRefresh()],
    esbuild: {
        jsxInject: `import React from 'react';`
      },
    server: {
        port: 8000,
    },
    build: {
        outDir: "../wwwroot/client",
        emptyOutDir: true
    },
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    }
})
