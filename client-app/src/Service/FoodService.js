import axios from "axios";

export async function getFoods(
    keyword = '',
    urlSlug='',
    categorySlug='',
    pageSize = 10,
    pageNumber = 1,
    sortColumn = '',
    sortOrder = '',
   
){
try {
    const response = await axios.get(
      `https://localhost:7027/api/Foods/?Keyword=${keyword}&UrlSlug=${urlSlug}&CategorySlug=${categorySlug}&PageSize=${pageSize}&PageNumber=${pageNumber}&SortColumn=${sortColumn}&SortOrder=${sortOrder}`
    );
    const data = response.data;
    if (data.isSuccess) return data.result;
    else return null;
  } catch (error) {
    console.log('Error', error.message);
    return null;
  }
}