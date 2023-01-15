import { error } from '@sveltejs/kit';
import axios, { AxiosError } from 'axios';

export const load = async () => {
    try {
        const config = {
            headers: {
                Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiSkFOIiwicm9sZSI6InVzZXIiLCJuYmYiOjE2NzM3Nzk5MTgsImV4cCI6MTY3Mzg2NjMxNywiaWF0IjoxNjczNzc5OTE4LCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.B41tDI7iMaSt3gzt1-MZEgNy2QhH4gaPiGukqdUxYaQ'
            }
        }
        const res = await axios.get('/api/users', config);
        return {
            status: 200,
            payload: res.data
        }
    } catch(err) {
        myerror(err);
    }
  }

  const myerror = (err) => {
    console.log('myerr', err);
    throw error(500, 'err');
  }