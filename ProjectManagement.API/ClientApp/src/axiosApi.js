import axios, {isAxiosError} from 'axios';
import {toast} from "react-toastify";

const axiosApi = axios.create({
    baseURL: 'api/',
});

axiosApi.interceptors.response.use(
    (response) => response,
    (error) => {

        if (isAxiosError(error)) {
            if (error.response.status >= 400 && error.response.status < 500) {
                toast.error(JSON.stringify(error.response.data.errors).replace(/[{}[\]"]/g, ' '));
            } else if (error.response.status >= 500) {
                toast.error('test test');
            }
            throw error;
        }
    }
)

export default axiosApi;