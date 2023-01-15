import adapter from '@sveltejs/adapter-static';
import { vitePreprocess } from '@sveltejs/kit/vite';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	kit: {
		adapter: adapter({
			pages: '../ptuswph-backend/wwwroot',
			assets: '../ptuswph-backend/wwwroot',
			fallback: "index.html",
			precompress: false,
			strict: true
		})
	},
	preprocess: vitePreprocess(),
};

export default config;
