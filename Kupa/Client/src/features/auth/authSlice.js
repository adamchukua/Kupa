import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { fetchWrapper } from './../fetchWrapper';

const initialState = {
  user: null,
  token: localStorage.getItem('token'),
  status: 'idle',
  error: null
};

export const login = createAsyncThunk('auth/login', async (userData, { rejectWithValue }) => {
  try {
    const data = await fetchWrapper.post('/Auth/login', userData);
    localStorage.setItem('token', data.token);
    return data;
  } catch (error) {
    return rejectWithValue(error.message);
  }
});

export const register = createAsyncThunk('auth/register', async (userData, { rejectWithValue }) => {
  try {
    const data = await fetchWrapper.post('/Auth/register', userData);
    localStorage.setItem('token', data.token);
    return data;
  } catch (error) {
    return rejectWithValue(error.message);
  }
});

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      localStorage.removeItem('token');
      state.user = null;
      state.token = null;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.fulfilled, (state, action) => {
        state.user = action.payload.user;
        state.token = action.payload.token;
      })
      .addCase(register.fulfilled, (state, action) => {
        state.user = action.payload.user;
        state.token = action.payload.token;
      });
  }
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
