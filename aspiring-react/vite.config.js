import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: process.env.PORT ? parseInt(process.env.PORT) : 4001,
    proxy: {
      '/api': {
        target: process.env.services__apiservice__https__0 || process.env.services__apiservice__http__0,
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ''),
        secure: false,
      },
    },
  },
})
