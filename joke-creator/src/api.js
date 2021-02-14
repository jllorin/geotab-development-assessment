import axios from 'axios';

export async function getCategories () {
   const url = `${process.env.REACT_APP_JOKE_API_ENDPOINT}/category`;
   // encode email for plus and other special character in email
   return axios.get(url)   
      .then(response => {
         return response.data;
      })
      .catch(err => {
         throw err;
      });
}

export async function getPerson () {
   const url = `${process.env.REACT_APP_JOKE_API_ENDPOINT}/person`;
   // encode email for plus and other special character in email
   return axios.get(url)   
      .then(response => {
         return response.data;
      })
      .catch(err => {
         throw err;
      });
}

export async function getJokes (option) {
   const url = `${process.env.REACT_APP_JOKE_API_ENDPOINT}/joke/generate`;
   // encode email for plus and other special character in email
   // return axios.post(url, option)   
   return axios.post(url, option)   
      .then(response => {         
         return response.data;
      })
      .catch(err => {
         throw err;
      });
}
