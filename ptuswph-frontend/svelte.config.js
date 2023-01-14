// svelte.config.js
import adapter from '@sveltejs/adapter-static';
import { vitePreprocess } from '@sveltejs/kit/vite';

export default {
  kit: {
    adapter: adapter({
      // default options are shown. On some platforms
      // these options are set automatically — see below
      pages: '../ptuswph-backend/wwwroot',
      assets: '../ptuswph-backend/wwwroot',
      fallback: "index.html",
      precompress: false,
      strict: true
    })
  },
  preprocess: vitePreprocess()
};