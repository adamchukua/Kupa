import React from 'react';
import EventsList from '../features/events/EventsList';

const Home = () => {
  return (
    <>
      <h1 className="text-3xl font-bold underline">
        Hello world!
      </h1>

      <EventsList />
    </>
  );
};

export default Home;
