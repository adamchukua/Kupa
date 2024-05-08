const baseUrl = 'https://localhost:7028/api';

export const fetchWrapper = {
  get(url) {
    const requestOptions = {
      method: 'GET',
      credentials: 'include',
      headers: { 'Content-Type': 'application/json-patch+json' },
    };
    return fetch(`${baseUrl}${url}`, requestOptions).then(handleResponse);
  },
  post(url, body) {
    const requestOptions = {
      method: 'POST',
      credentials: 'include',
      headers: { 'Content-Type': 'application/json-patch+json' },
      body: body
    };
    return fetch(`${baseUrl}${url}`, requestOptions).then(handleResponse);
  }
};

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }
    return data;
  });
}
