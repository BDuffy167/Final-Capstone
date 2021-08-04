import axios from 'axios';


export default {

   get(id){
       return axios.get(`/ReadingLog/${id}`);
   }
}
//add(book)
//get(notes)--display notes
//displayActivity
//displayBooks