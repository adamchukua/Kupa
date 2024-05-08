import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { fetchWrapper } from './../fetchWrapper';

export const fetchCommentsByEventId = createAsyncThunk(
  'comments/fetchComments',
  async (eventId, { rejectWithValue }) => {
    try {
      const response = await fetchWrapper.get(`/events/${eventId}/comments`);
      if (!response.ok) throw new Error('Network response was not ok');
      return response.json();
    } catch (error) {
      return rejectWithValue(error.message);
    }
  }
);

export const postComment = createAsyncThunk(
  'comments/postComment',
  async ({ eventId, comment }, { rejectWithValue }) => {
    try {
      const response = await fetchWrapper.post(`/EventComment/event/${eventId}`, JSON.stringify({ comment: comment }));
      if (!response.ok) throw new Error('Failed to post comment');
      return response.json();
    } catch (error) {
      return rejectWithValue(error.message);
    }
  }
);

const commentsSlice = createSlice({
  name: 'comments',
  initialState: {
    comments: [],
    loading: false,
    error: null,
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCommentsByEventId.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchCommentsByEventId.fulfilled, (state, action) => {
        state.loading = false;
        state.comments = action.payload;
      })
      .addCase(fetchCommentsByEventId.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      })
      .addCase(postComment.fulfilled, (state, action) => {
        state.comments.push(action.payload);
      });
  }
});

export const { reducer } = commentsSlice;
export default reducer;
