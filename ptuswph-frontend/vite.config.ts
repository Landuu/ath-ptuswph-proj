import { sveltekit } from '@sveltejs/kit/vite';
import path from 'path';
import type { UserConfig } from 'vite';

const config: UserConfig = {
	plugins: [sveltekit()],
	resolve: {
		alias: {
			'@' : path.resolve(__dirname, './src')
		}
	},
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
