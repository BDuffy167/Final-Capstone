import axios from 'axios';


export default {

   get(id){
       return axios.get(`/ReadingLog/${id}`);
   },
   post(familyID, newBook) {
       return axios.post(`/Book/${familyID}/AddBook`, 
       {
        bookId: 0,
        title: newBook.title,
        authorFirstName: newBook.authorFirstName,
        authorLastName: newBook.authorLastName,
        isbn: newBook.isbn
       })
   }
}
//add(book)
//get(notes)--display notes
//displayActivity
//displayBooks