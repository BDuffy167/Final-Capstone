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
<<<<<<< HEAD
        personalLibraryId: newLog.personalLibraryID,
        formatType: newLog.formatType,
        totalTime: newLog.timeRead,
=======
        personalLibraryId: newLog.personalLibraryId,
        formatType: newLog.formatType,
        totalTime: newLog.totalTime,
>>>>>>> b46b10ca7d6d963dce6096554aed85041c6b7219
        note: newLog.note
       })
   },
   getUserHistory(userId){
       return axios.get(`/ReadingLog/${userId}/UserHistory`);
       
   },
   getPersonalLibrary(userId) {
       return axios.get(`Book/${userId}/GetPersonalBooks`);
   }
}
//add(book)
//get(notes)--display notes
//displayActivity
//displayBooks