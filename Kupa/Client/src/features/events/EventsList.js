import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { fetchEvents } from './eventsSlice';

const EventsList = () => {
  const dispatch = useDispatch();
  const { events, loading, error } = useSelector((state) => state.events);

  useEffect(() => {
    dispatch(fetchEvents());
  }, [dispatch]);

  return (
    <div className="container mx-auto p-4">
      {loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p className="text-red-500">Error: {error}</p>
      ) : (
        <div className="grid grid-cols-3 gap-4">
          {events.map((event) => (
            <div key={event.id} className="bg-white p-4 shadow rounded">
              <a href={"events/" + event.id}>
                <h2 className="font-bold text-lg">{event.title}</h2>
                <p>{event.description}</p>
              </a>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default EventsList;
