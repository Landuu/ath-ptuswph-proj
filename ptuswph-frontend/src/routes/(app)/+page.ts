import type { PageLoad } from './$types';
import { getAuthOptions } from '@/utils';
import { error } from '@sveltejs/kit';
import type { ApiMovie } from '@/types';


export const load = (async ({ fetch }) => {
	const res = await fetch(`/api/movies`, getAuthOptions());
	if(res.status != 200) {
		console.log('sts', res.status);
		throw error(res.status, 'Błąd podczas pobierania danych!');
	}
	const data = res.json();
	return { data };
}) satisfies PageLoad;