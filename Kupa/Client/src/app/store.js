import { configureStore } from '@reduxjs/toolkit';
import counterReducer from '../features/counter/counterSlice';
import eventsReducer from '../features/events/eventsSlice';
import commentReducer from '../features/comments/commentsSlice';

export default configureStore({
  reducer: {
    counter: counterReducer,
    events: eventsReducer,
    comments: commentReducer,
  }
})