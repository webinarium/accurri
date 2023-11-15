/**
 * Generates unique ID.
 *
 * @param {string} prefix Optional prefix for the ID
 *
 * @return {string} Generated ID
 */
export default (prefix = '__accurri_') => prefix + Math.random().toString(36).substring(2);
