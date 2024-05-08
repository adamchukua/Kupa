import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import { fetchEventById } from './eventsSlice';

const EventDetailsPage = () => {
  const { eventId } = useParams();
  const dispatch = useDispatch();
  const event = useSelector(state => state.events.currentEvent);
  const loading = useSelector(state => state.events.loading);
  const error = useSelector(state => state.events.error);

  useEffect(() => {
    dispatch(fetchEventById(eventId));
  }, [dispatch, eventId]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error}</p>;
  if (!event) return <p>No event found!</p>;

  return (
    <div className="p-4 max-w-4xl mx-auto">
      <h1 className="text-xl font-bold">{event.title}</h1>
      <p>{event.description}</p>
      <p><b>Status:</b> {event.status.name}</p>
      <p><b>Organisator:</b> {event.user?.profile?.name}</p>
      <p><b>Category:</b> {event.category}</p>
      <p><b>Created:</b> {event.createdAt}</p>
      <p><b>Description:</b> {event.description}</p>
      <p><b>Location:</b> {event.location.city} {event.location.address}</p>
      <br/>
      {event.eventComments && event.eventComments.length > 0 ? (
        <>
            <h1 className="text-xl font-bold">Comments</h1>

            <ul>
            {event.eventComments.map(comment => (
                <li key={comment.id} className="bg-gray-100 rounded p-3 my-2">
                <p className="text-sm text-gray-600">{comment.postedBy} on {new Date(comment.postedAt).toLocaleDateString()}</p>
                <p>{comment.text}</p>
                </li>
            ))}
            </ul>
        </>
      ) : (
        <p>No comments yet.</p>
      )}
    </div>
  );
};

export default EventDetailsPage;
