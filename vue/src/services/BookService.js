import axios from 'axios';


export default {
    getFamilyBooks(familyId){
        return axios.get(`/Book/${familyId}/GetFamilyBooks`);
    },
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
   },
   postLog(userId, newLog){
       return axios.post(`/ReadingLog/${userId}/AddLog`,
       {
        logID: 0,
        personalLibraryId: newLog.personalLibraryId,
        formatType: newLog.formatType,
        totalTime: newLog.totalTime,
        note: newLog.note
       })
   },
   getUserHistory(userId){
       return axios.get(`/ReadingLog/${userId}/UserHistory`);
       
   },
   getPersonalLibrary(userId) {
       return axios.get(`Book/${userId}/GetPersonalBooks`);
   },
   postPersonalBook(userId, bookId){
       return axios.post(`Book/${userId}/AddPersonalBook`, bookId)
   }
}
//add(book)
//get(notes)--display notes
//displayActivity
//displayBooks