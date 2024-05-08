import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';

export const fetchEvents = createAsyncThunk(
  'events/fetchEvents',
  async (_, { rejectWithValue }) => {
    try {
      const response = await fetch('https://localhost:7028/api/Events');
      if (!response.ok) throw new Error('Network response was not ok');
      return response.json();
    } catch (error) {
      return rejectWithValue(error.message);
    }
  }
);

export const fetchEventById = createAsyncThunk('events/fetchEventById', async (eventId, { rejectWithValue }) => {
    try {
        const response = await fetch(`https://localhost:7028/api/Events/${eventId}`);
        if (!response.ok) throw new Error('Failed to fetch');
        return response.json();
    } catch (error) {
        return rejectWithValue(error.message);
    }
});

const eventsSlice = createSlice({
  name: 'events',
  initialState: {
    events: [],
    currentEvent: null,
    loading: false,
    error: null
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      // fetchEvents
      .addCase(fetchEvents.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchEvents.fulfilled, (state, action) => {
        state.loading = false;
        state.events = action.payload;
      })
      .addCase(fetchEvents.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      })
      // fetchEventById
      .addCase(fetchEventById.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchEventById.fulfilled, (state, action) => {
        state.loading = false;
        state.currentEvent = action.payload;
      })
      .addCase(fetchEventById.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      });
  }
});

export default eventsSlice.reducer;
