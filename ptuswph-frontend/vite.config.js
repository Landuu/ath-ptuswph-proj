import { sveltekit } from '@sveltejs/kit/vite';

/** @type {import('vite').UserConfig} */
const config = {
	plugins: [sveltekit()],
	server: {
		proxy: {
			'/api': {
				target: 'http://localhost:8000/api',
				changeOrigin: true,
				rewrite: (path) => path.replace(/^\/api/, ''),
			},
		}
	}
};

export default config;
