import axios from 'axios';

export default {
    GetAllBooks(){

        return axios.get('/ReadingLog/ID');
    }
}