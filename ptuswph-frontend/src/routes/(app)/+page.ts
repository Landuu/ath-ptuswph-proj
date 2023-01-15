import type { PageLoad } from './$types';
import { getAuthHeaders } from '@/utils';
import { error } from '@sveltejs/kit';


export const load = (async ({ fetch }) => {
	const res = await fetch(`/api/users`, getAuthHeaders());
	if(res.status != 200) {
		console.log('sts', res.status);
		throw error(res.status, 'Błąd podczas pobierania danych!');
	}
	const data = res.json();
	return { data };
}) satisfies PageLoad;